/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web.Services.Description;
using Microsoft.CSharp;

namespace WLib.Web
{
    /// <summary>
    /// 动态创建调用WebService的代理类，提供调用WebService服务的方法
    /// </summary>
    public class WebServiceAgent
    {
        /// <summary>
        /// Web服务Url
        /// </summary>
        public string ServiceUrl { get; }
        /// <summary>
        /// 客户端代理服务的命名空间
        /// </summary>
        public string LocalNamespace { get; }

        /// <summary>
        /// 客户端代理类的类名
        /// </summary>
        public string ClassName { get; private set; }
        /// <summary>
        /// 客户端代理类的实例
        /// </summary>
        public object ServiceInstance { get; private set; }
        /// <summary>
        /// 客户端代理类对应的Type
        /// </summary>
        public Type ServiceType { get; private set; }
        /// <summary>
        /// Web服务提供的方法
        /// </summary>
        public MethodInfo[] ServiceMethodInfos => ServiceType.GetMethods();

        /// <summary>
        /// 动态创建调用WebService的代理类，提供调用WebService服务的方法
        /// </summary>
        /// <param name="serviceUrl">Web服务Url</param>
        /// <param name="localNamespace">客户端代理服务的命名空间</param>
        public WebServiceAgent(string serviceUrl, string localNamespace = "MyServiceReference")
        {
            this.ServiceUrl = serviceUrl;
            this.LocalNamespace = localNamespace;

            CreateServiceAssembly();
        }
        /// <summary>
        /// 读取webService的WSDL，根据WSDL创建调用WebService的代理类
        /// </summary>
        private void CreateServiceAssembly()
        {
            //获取WSDL
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(ServiceUrl + "?WSDL");
            ServiceDescription serviceDesc = ServiceDescription.Read(stream);//服务的描述信息都可以通过ServiceDescription获取  
            ClassName = serviceDesc.Services[0].Name;

            ServiceDescriptionImporter sdImporter = new ServiceDescriptionImporter();
            sdImporter.AddServiceDescription(serviceDesc, "", "");
            CodeNamespace codeNamespace = new CodeNamespace(LocalNamespace);

            //生成客户端代理类代码  
            CodeCompileUnit codeComplieUnit = new CodeCompileUnit();
            codeComplieUnit.Namespaces.Add(codeNamespace);
            sdImporter.Import(codeNamespace, codeComplieUnit);
            CSharpCodeProvider csCodeProvider = new CSharpCodeProvider();

            //设定编译参数  
            CompilerParameters complierParameters = new CompilerParameters();
            complierParameters.GenerateExecutable = false;
            complierParameters.GenerateInMemory = true;
            complierParameters.ReferencedAssemblies.Add("System.dll");
            complierParameters.ReferencedAssemblies.Add("System.XML.dll");
            complierParameters.ReferencedAssemblies.Add("System.Web.Services.dll");
            complierParameters.ReferencedAssemblies.Add("System.Data.dll");

            //编译代理类  
            CompilerResults complierResults = csCodeProvider.CompileAssemblyFromDom(complierParameters, codeComplieUnit);
            if (complierResults.Errors.HasErrors == true)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (CompilerError compilerError in complierResults.Errors)
                {
                    sb.Append(compilerError);
                    sb.Append(Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            //生成代理实例，并调用方法  
            Assembly assembly = complierResults.CompiledAssembly;
            ServiceType = assembly.GetType(LocalNamespace + "." + ClassName, true, true);
            ServiceInstance = Activator.CreateInstance(ServiceType);
        }

        /// <summary>
        /// 调用服务的具体方法
        /// </summary>
        /// <param name="methodName">服务方法的名称</param>
        /// <param name="parameters">服务方法的参数，没有参数则应设置为null</param>
        /// <returns></returns>
        public object InvokeMethod(string methodName, object[] parameters)
        {
            MethodInfo methodInfo = ServiceType.GetMethod(methodName);
            return methodInfo.Invoke(ServiceInstance, parameters);
        }
    }
}