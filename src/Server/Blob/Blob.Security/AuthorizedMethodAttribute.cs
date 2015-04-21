//using System;
//using System.ServiceModel.Channels;
//using System.ServiceModel.Description;
//using System.ServiceModel.Dispatcher;

//namespace Blob.Security
//{
//    public class AuthorizedMethodAttribute : Attribute, IOperationBehavior
//    {
//        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
//        { }

//        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
//        { }

//        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
//        {
//            IOperationInvoker defaultInvoker = dispatchOperation.Invoker;
//            dispatchOperation.Invoker = new AuthenticationOperationInvoker(defaultInvoker);
//        }

//        public void Validate(OperationDescription operationDescription)
//        { }
//    }
//}
