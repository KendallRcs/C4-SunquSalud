using Structurizr;

namespace c4_model_design
{
	public class HelpCenterBCComponentDiagram
    {
		private readonly C4 c4;
		private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";

        public Component DomainLayer { get; private set; }
        public Component InterfaceLayer { get; private set; }
        public Component ApplicationLayer { get; private set; }
        public Component InfrastructureLayer { get; private set; }

        public HelpCenterBCComponentDiagram(C4 c4, ContainerDiagram containerDiagram)
		{
			this.c4 = c4;
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
            DomainLayer = containerDiagram.ApiRest.AddComponent("Domain Layer Help Center", "", "NodeJS (NestJS)");
            InterfaceLayer = containerDiagram.ApiRest.AddComponent("Interface Layer Help Center", "", "NodeJS (NestJS)");
            ApplicationLayer = containerDiagram.ApiRest.AddComponent("Application Layer Help Center", "", "NodeJS (NestJS)");
            InfrastructureLayer = containerDiagram.ApiRest.AddComponent("Infrastructure Layer Help Center", "", "NodeJS (NestJS)");
        }

        private void AddRelationships() {
            InterfaceLayer.Uses(ApplicationLayer, "", "");
            ApplicationLayer.Uses(DomainLayer, "", "");
            ApplicationLayer.Uses(InfrastructureLayer, "", "");
            InfrastructureLayer.Uses(DomainLayer, "", "");
            InfrastructureLayer.Uses(containerDiagram.Database, "Usa", "");
        }

		private void ApplyStyles() {
			SetTags();
		}

		private void SetTags()
		{
            DomainLayer.AddTags(this.componentTag);
            InterfaceLayer.AddTags(this.componentTag);
            ApplicationLayer.AddTags(this.componentTag);
            InfrastructureLayer.AddTags(this.componentTag);
        }

		private void CreateView() {
            string title = "Help Center BC Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
            componentView.Add(containerDiagram.Database);
			componentView.Add(this.DomainLayer);
			componentView.Add(this.InterfaceLayer);
			componentView.Add(this.ApplicationLayer);
			componentView.Add(this.InfrastructureLayer);
		}
	}
}