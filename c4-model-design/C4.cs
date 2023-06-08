using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
	public class C4
	{
		private readonly long workspaceId = 83572;
		private readonly string apiKey = "0556d671-e1dd-4302-959d-172229220d08";
		private readonly string apiSecret = "09521061-e8f4-48fe-aa08-a11965e92400";

		public StructurizrClient StructurizrClient { get; }
		public Workspace Workspace { get; }
		public Model Model { get; }
		public ViewSet ViewSet { get; }

		public C4()
		{
			string workspaceName = "SunquSalud - C4 Model - Sistema de Monitoreo";
			string workspaceDescription = "Sistema de Monitoreo de Donaciones de Sangre";
			StructurizrClient = new StructurizrClient(apiKey, apiSecret);
			Workspace = new Workspace(workspaceName, workspaceDescription);
			Model = Workspace.Model;
			ViewSet = Workspace.Views;
		}

		public void Generate() {
			ContextDiagram contextDiagram = new ContextDiagram(this);
			ContainerDiagram containerDiagram = new ContainerDiagram(this, contextDiagram);
            APIRestComponentDiagram apiRestComponentDiagram = new APIRestComponentDiagram(this, contextDiagram, containerDiagram);
			MedicalCenterComponentDiagram medicalCenterComponentDiagram = new MedicalCenterComponentDiagram(this, contextDiagram, containerDiagram);
            CalendarBCComponentDiagram calendarComponentDiagram = new CalendarBCComponentDiagram(this, containerDiagram);
            DonationsBCComponentDiagram donationsComponentDiagram = new DonationsBCComponentDiagram(this, containerDiagram);
            HelpCenterBCComponentDiagram helpCenterComponentDiagram = new HelpCenterBCComponentDiagram(this, containerDiagram);
            LogginBCComponentDiagram logginInventoryComponentDiagram = new LogginBCComponentDiagram(this, containerDiagram);
            MedicalHistoryBCComponentDiagram medicalHistoryComponentDiagram = new MedicalHistoryBCComponentDiagram(this, containerDiagram);
            UsersBCComponentDiagram usersHistoryComponentDiagram = new UsersBCComponentDiagram(this, containerDiagram);
			contextDiagram.Generate();
			containerDiagram.Generate();
			apiRestComponentDiagram.Generate();
            medicalCenterComponentDiagram.Generate();
			calendarComponentDiagram.Generate();
            donationsComponentDiagram.Generate();
			helpCenterComponentDiagram.Generate();
            logginInventoryComponentDiagram.Generate();
            medicalHistoryComponentDiagram.Generate();
            usersHistoryComponentDiagram.Generate();
			StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}