using System;

namespace SalesWebMvc247.Models.ViewModels
{
    public class ErrorViewModel
    {
        //id interno da requisi��o  ------RequestId
        public string RequestId { get; set; }
        //acresentar uma messagem custumizada neste objeto
        public string Message { get; set; }
        /// <summary>
        // retorna pra fun��o  com um massete pra ver se existe id interno se n�o � nulo ou vazio----- !string.IsNullOrEmpty(RequestId);
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
