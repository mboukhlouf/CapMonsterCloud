using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CapMonsterCloud.Exceptions;
using CapMonsterCloud.Models.CaptchaTasks;
using CapMonsterCloud.Models.CaptchaTasksResults;
using CapMonsterCloud.Models.Requests;
using CapMonsterCloud.Models.Responses;

namespace CapMonsterCloud
{
    public class CapMonsterClient : IDisposable
    {
        private readonly ApiProcessor apiProcessor;

        private bool disposed = false;

        private const int GetTaskResultDelay = 100; 

        public string Token
        {
            get => apiProcessor.Key;
            set => apiProcessor.Key = value;
        }

        public CapMonsterClient()
        {
            apiProcessor = new ApiProcessor();
        }

        public CapMonsterClient(string token) : this()
        {
            Token = token;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    apiProcessor.Dispose();
                }

                disposed = true;
            }
        }

        ~CapMonsterClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Creates a task for solving captcha
        /// </summary>
        /// <param name="task">Task object</param>
        /// <returns>Task Id to be used in GetTaskResultAsync</returns>
        public Task<int> CreateTaskAsync(CaptchaTask task)
        {
            return CreateTaskAsync(task, CancellationToken.None);
        }

        /// <summary>
        /// Creates a task for solving captcha
        /// </summary>
        /// <param name="task">Task object</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns></returns>
        public async Task<int> CreateTaskAsync(CaptchaTask task, CancellationToken cancellationToken)
        {
            var endpoint = Endpoints.CreateTask();
            var requestObject = new CreateTaskRequest
            {
                Task = task
            };
            var response = await apiProcessor.ProcessRequestAsync<CreateTaskResponse>(endpoint, requestObject, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
            return response.TaskId;
        }

        /// <summary>
        /// Get the result of the task
        /// </summary>
        /// <typeparam name="CaptchaTaskResultT">The type of the task result</typeparam>
        /// <param name="taskId">The task id which was obtained using CreateTaskAsync</param>
        /// <returns>The task result</returns>
        public Task<CaptchaTaskResultT> GetTaskResultAsync<CaptchaTaskResultT>(int taskId) where CaptchaTaskResultT : CaptchaTaskResult
        {
            return GetTaskResultAsync<CaptchaTaskResultT>(taskId, CancellationToken.None);
        }

        /// <summary>
        /// Get the result of the task
        /// </summary>
        /// <typeparam name="CaptchaTaskResultT">The type of the task result</typeparam>
        /// <param name="taskId">The task id which was obtained using CreateTaskAsync</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns>The task result</returns>
        public async Task<CaptchaTaskResultT> GetTaskResultAsync<CaptchaTaskResultT>(int taskId, CancellationToken cancellationToken) where CaptchaTaskResultT : CaptchaTaskResult
        {
            var endpoint = Endpoints.GetTaskResult();
            var requestObject = new GetTaskResultRequest
            {
                TaskId = taskId
            };
            GetTaskResultResponse<CaptchaTaskResultT> response;
            while (true)
            {
                response = await apiProcessor.ProcessRequestAsync<GetTaskResultResponse<CaptchaTaskResultT>>(endpoint, requestObject, cancellationToken).ConfigureAwait(false);
                response.EnsureSuccess();
                if (response.Status == CaptchaTaskStatus.Ready)
                    break;
                await Task.Delay(GetTaskResultDelay, cancellationToken).ConfigureAwait(false);
            }
            return response.Solution;
        }

        /// <summary>
        /// Get the available balance in the account
        /// </summary>
        /// <returns>The balance available in the account</returns>
        public Task<decimal> GetBalanceAsync()
        {
            return GetBalanceAsync(CancellationToken.None);
        }

        /// <summary>
        /// Get the available balance in the account
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation</param>
        /// <returns>The balance available in the account</returns>
        public async Task<decimal> GetBalanceAsync(CancellationToken cancellationToken)
        {
            var endpoint = Endpoints.GetBalance();
            var response = await apiProcessor.ProcessRequestAsync<GetBalanceResponse>(endpoint, new GetBalanceRequest(), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
            return response.Balance;
        }
    }
}
