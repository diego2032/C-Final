﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ActualFinalProject.CalculateTotalsReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CalculateTotalsReference.CalculateTotalsSoap")]
    public interface CalculateTotalsSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalculateTotal", ReplyAction="*")]
        double CalculateTotal(double subtotal);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/CalculateTotal", ReplyAction="*")]
        System.Threading.Tasks.Task<double> CalculateTotalAsync(double subtotal);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CalculateTotalsSoapChannel : ActualFinalProject.CalculateTotalsReference.CalculateTotalsSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculateTotalsSoapClient : System.ServiceModel.ClientBase<ActualFinalProject.CalculateTotalsReference.CalculateTotalsSoap>, ActualFinalProject.CalculateTotalsReference.CalculateTotalsSoap {
        
        public CalculateTotalsSoapClient() {
        }
        
        public CalculateTotalsSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CalculateTotalsSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculateTotalsSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculateTotalsSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double CalculateTotal(double subtotal) {
            return base.Channel.CalculateTotal(subtotal);
        }
        
        public System.Threading.Tasks.Task<double> CalculateTotalAsync(double subtotal) {
            return base.Channel.CalculateTotalAsync(subtotal);
        }
    }
}
