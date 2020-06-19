using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CapMonsterNet.Exceptions;
using CapMonsterNet.Models.CaptchaTasks;
using CapMonsterNet.Models.CaptchaTasksResults;
using CapMonsterNet.Models.Requests;
using CapMonsterNet.Models.Responses;

namespace CapMonsterNet
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
        /// <returns>Task Id to be used in GetTaskResultAsync</returns>
        public async Task<int> CreateTaskAsync(CaptchaTask task)
        {
            var endpoint = Endpoints.CreateTask();
            var requestObject = new CreateTaskRequest
            {
                Task = task
            };
            var response = await apiProcessor.ProcessRequestAsync<CreateTaskResponse>(endpoint, requestObject);
            response.EnsureSuccess();
            return response.TaskId;
        }

        /// <summary>
        /// Get the result of the task
        /// </summary>
        /// <typeparam name="CaptchaTaskResultT">The type of the task result</typeparam>
        /// <param name="taskId">The task id which was obtained using CreateTaskAsync</param>
        /// <returns>The task result</returns>
        public async Task<CaptchaTaskResultT> GetTaskResultAsync<CaptchaTaskResultT>(int taskId) where CaptchaTaskResultT : CaptchaTaskResult
        {
            var endpoint = Endpoints.GetTaskResult();
            var requestObject = new GetTaskResultRequest
            {
                TaskId = taskId
            };
            GetTaskResultResponse<CaptchaTaskResultT> response;
            while (true)
            {
                response = await apiProcessor.ProcessRequestAsync<GetTaskResultResponse<CaptchaTaskResultT>>(endpoint, requestObject);
                response.EnsureSuccess();
                if (response.Status == CaptchaTaskStatus.Ready)
                    break;
                await Task.Delay(GetTaskResultDelay);
            }
            return response.Solution;
        }

        /// <summary>
        /// Get the available balance in the account
        /// </summary>
        /// <returns>The balance available in the account</returns>
        public async Task<decimal> GetBalanceAsync()
        {
            var endpoint = Endpoints.GetBalance();
            var response = await apiProcessor.ProcessRequestAsync<GetBalanceResponse>(endpoint, new GetBalanceRequest());
            response.EnsureSuccess();
            return response.Balance;
        }
    }
}
