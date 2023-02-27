using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace setStatusReasonPlugin
{
    public class SRPLugin : IPlugin
    {

        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the tracing service
            ITracingService tracingService =
            (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // The InputParameters collection contains all the data passed in the message request.  
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {

                // Obtain the target entity from the input parameters.  
                Entity entity = (Entity)context.InputParameters["Target"];

                // Obtain the organization service reference which you will need for  
                // web service calls.  
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {
                    // Plug-in business logic goes here.  

                   
                    
                    // Verify all the requirements for the step registration
                    if (context.InputParameters.Contains("Target") && //Is a message with Target
                        context.InputParameters["Target"] is Entity && //Target is an entity
                                                                       // Instantiate QueryExpression query
            


                    ((Entity)context.InputParameters["Target"]).LogicalName.Equals("contact") && //Target is an account
                        ((Entity)context.InputParameters["Target"])["statuscode"] != null && //status code name is passed
                        context.MessageName.Equals("Update")) //account name included with PreEntityImage with step
                    {

                        Entity lead = (Entity)context.InputParameters["Target"];

                        Entity preLead = (Entity)context.PreEntityImages["Image"];

                        Entity postLead = (Entity)context.PostEntityImages["Image"];
                        var preleadvalue = ((OptionSetValue)preLead.Attributes["statuscode"]).Value;
                        var postleadValue = ((OptionSetValue)postLead.Attributes["statuscode"]).Value;


                        Entity mainEntity = new Entity("account");
                        
                        int statusCodeValue = mainEntity.GetAttributeValue<int>("statuscode");
                        tracingService.Trace("setting the login in status reason");
                        if ((postleadValue == 1 && preleadvalue == 933810000)|| (postleadValue == 933810000 && preleadvalue == 933810001) || (postleadValue == 1 && preleadvalue == 933810001))
                        {

                            throw new InvalidPluginExecutionException("oye!! hushh hushh...      You do not allow to move backward");
                            //change 
                            
                        }
                        //else if (preleadvalue == 933810000 && postleadValue == 933810001)
                        //{
                        //    //change
                        //    statusCodeValue = 933810001;

                        //}
                        //else if (preleadvalue == 1 && postleadValue == 933810001)
                        //{
                        //    //change
                        //    statusCodeValue = 933810001;
                        //}
                        //else
                        //{
                        //    statusCodeValue = preleadvalue;
                        //}
                       // Entity mainEntity = new Entity("account");
                       //
                       // string statusCodeValue = mainEntity.GetAttributeValue<string>("statuscode");
                      /* if(entity.LogicalName == "account")
                        {
                            entity.Attributes.Contains("statusCode");
                            //getiing the value 
                            var CurrentValueOfSR = entity["statuscode"].ToString();



                            if (CurrentValueOfSR == "Active")
                            {
                                entity["statuscode"] = "Inprocess";
                            }
                            else if(CurrentValueOfSR == "Inprocess")
                            {
                                entity["statuscode"] = "Completed";
                            }


                        }*/

                       /*
                        if (statusCodeValue == "Active")
                        {
                            tracingService.Trace("ValidateAccountName: Testing for {0} invalid names:", invalidNames.Count);
                       //SET INPROCESSS
                        }
                        else if (statusCodeValue == "inprocess")
                        {
                            // SET COMPLETED
                        }
                        else 
                        {

                        }
                       */
                    }
                    else
                    {
                        tracingService.Trace("ValidateAccountName: The step for this plug-in is not configured correctly.");
                    }









                }

                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in FollowUpPlugin.", ex);
                }

                catch (Exception ex)
                {
                    tracingService.Trace("FollowUpPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
    
}

















/*
 /Get and Set Single Line of Text field value
// Display Name: "Account Number" | Database Name: "accountnumber"
string statusReasonFieldLogicalName = "statuscode";
Object statusReasonObj;
string statusReasonNumber;

//Check if the specified attribute is contained in the internal dictionary before you you try to Get its value
if (primaryEntity.Attributes.TryGetValue(statusReasonFieldLogicalName, out accountNumberObj))
{
	//Get value of Single Line of Text field
	statusReasonNumber = Convert.ToString(statusReasonObj);
if(statusReasonNumber == "Active"){

}elseif(){

}else{

}
	//Set value of Single Line of Text field
	primaryEntity[statusReasonFieldLogicalName] = statusReasonNumber;
}
 */