using System;

namespace SalesWebMvc247.Models.ViewModels
{
    public class ErrorViewModel
    {
        //id interno da requisição  ------RequestId
        public string RequestId { get; set; }
        //acresentar uma messagem custumizada neste objeto
        public string Message { get; set; }
        /// <summary>
        // retorna pra função  com um massete pra ver se existe id interno se não é nulo ou vazio----- !string.IsNullOrEmpty(RequestId);
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
