using BaseArchitecture.ExternalServices.Mail.Models;
using MediatR;
using Reec.Inspection;
using RulesEngine.Models;

namespace BaseArchitecture.Application.Handlers.Commands.DemoWorkflowController.MayorDeEdad
{
    public class MayorDeEdad : IRequest<object>
    {

        public MayorDeEdad(List<MayorDeEdadRequest> mayorDeEdadRequests)
        {
            MayorDeEdadRequests = mayorDeEdadRequests;
        }

        public List<MayorDeEdadRequest> MayorDeEdadRequests { get; }


        public class MayorDeEdadHandler : IRequestHandler<MayorDeEdad, object>
        {

            public MayorDeEdadHandler()
            {

            }

            public Task<object> Handle(MayorDeEdad request, CancellationToken cancellationToken)
            {



                List<Rule> rules = new List<Rule>();
                Rule rule = new Rule();
                rule.RuleName = "Test Rule";
                rule.SuccessEvent = "Count is within tolerance.";
                rule.ErrorMessage = "Over expected.";
                rule.Expression = "count < 3";
                rule.RuleExpressionType = RuleExpressionType.LambdaExpression;
                rules.Add(rule);

                var workflows = new List<Workflow>();

                Workflow exampleWorkflow = new Workflow();
                exampleWorkflow.WorkflowName = "Example Workflow";
                exampleWorkflow.Rules = rules;

                workflows.Add(exampleWorkflow);

                var bre = new RulesEngine.RulesEngine(workflows.ToArray());







                throw new NotImplementedException();



            }
        }

    }
}
