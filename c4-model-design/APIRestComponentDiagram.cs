using Structurizr;

namespace c4_model_design
{
	public class APIRestComponentDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";
        public Component MedicalHistory { get; private set; }
        public Component Users { get; private set; }
		public Component Loggin { get; private set; }
        public Component HelpCenter { get; private set; }
		public Component MedicalCenter { get; private set; }
		public Component Calendar { get; private set; }
		public Component Donations { get; private set; }
		public Component SharedKernel { get; private set; }

		public APIRestComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
			this.containerDiagram = containerDiagram;
		}

		public void Generate() {
			AddComponents();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddComponents()
		{
			SharedKernel = containerDiagram.ApiRest.AddComponent("Shared Kernel", "", "NodeJS (NestJS)");

			MedicalHistory = containerDiagram.ApiRest.AddComponent("Medical History", "", "NodeJS (NestJS)");
            Users = containerDiagram.ApiRest.AddComponent("Users", "", "NodeJS (NestJS)");
            Loggin = containerDiagram.ApiRest.AddComponent("Loggin", "", "NodeJS (NestJS)");
            HelpCenter = containerDiagram.ApiRest.AddComponent("HelpCenter", "", "NodeJS (NestJS)");
			MedicalCenter = containerDiagram.ApiRest.AddComponent("MedicalCenter", "", "NodeJS (NestJS)");
			Calendar = containerDiagram.ApiRest.AddComponent("Calendar", "", "NodeJS (NestJS)");
			Donations = containerDiagram.ApiRest.AddComponent("Donations", "", "NodeJS (NestJS)");
		}

		private void AddRelationships() {
			MedicalHistory.Uses(containerDiagram.Database, "Usa", "");
			MedicalHistory.Uses(this.SharedKernel, "Usa", "");

			Users.Uses(containerDiagram.Database, "Usa", "");
			Users.Uses(this.SharedKernel, "Usa", "");
			
			Loggin.Uses(containerDiagram.Database, "Usa", "");
			Loggin.Uses(this.SharedKernel, "Usa", "");

			MedicalCenter.Uses(containerDiagram.Database, "Usa", "");
			MedicalCenter.Uses(this.SharedKernel, "Usa", "");
			MedicalCenter.Uses(contextDiagram.GoogleMaps, "Usa", "");
			MedicalCenter.Uses(contextDiagram.DonationSystem, "Usa", "");
			
			HelpCenter.Uses(containerDiagram.Database, "Usa", "");
			HelpCenter.Uses(this.SharedKernel, "Usa", "");

			Calendar.Uses(containerDiagram.Database, "Usa", "");
			Calendar.Uses(this.SharedKernel, "Usa", "");

			Donations.Uses(containerDiagram.Database, "Usa", "");
			Donations.Uses(this.SharedKernel, "Usa", "");
        }

        private void ApplyStyles() {
			SetTags();
			Styles styles = c4.ViewSet.Configuration.Styles;
			styles.Add(new ElementStyle(this.componentTag) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
		}

		private void SetTags()
		{
			MedicalHistory.AddTags(this.componentTag);
            Users.AddTags(this.componentTag);
            Loggin.AddTags(this.componentTag);
            MedicalCenter.AddTags(this.componentTag);
			HelpCenter.AddTags(this.componentTag);
			Calendar.AddTags(this.componentTag);
			Donations.AddTags(this.componentTag);
			SharedKernel.AddTags(this.componentTag);
		}

		private void CreateView() {
			string title = "API Rest Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
			componentView.Add(containerDiagram.Database);
			componentView.Add(contextDiagram.DonationSystem);
			componentView.Add(contextDiagram.GoogleMaps);
			componentView.AddAllComponents();
		}
	}
}