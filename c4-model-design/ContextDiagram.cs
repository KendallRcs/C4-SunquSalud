using Structurizr;

namespace c4_model_design
{
	public class ContextDiagram
	{
		private readonly C4 c4;
		public SoftwareSystem MonitoringSystem { get; private set; }
		public SoftwareSystem GoogleMaps { get; private set; }
        public SoftwareSystem GoogleCalendar { get; private set; }
        public SoftwareSystem DonationSystem { get; private set; }
        public Person Admin { get; private set; }
        public Person UsuarioReceptor { get; private set; }
        public Person UsuarioDonante { get; private set; }
        public Person Hospital { get; private set; }
        public ContextDiagram(C4 c4)
		{
			this.c4 = c4;
		}

		public void Generate() {
			AddElements();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddElements() {
			AddPeople();
			AddSoftwareSystems();
		}

		private void AddPeople()
		{
            UsuarioReceptor = c4.Model.AddPerson("Usuario Receptor", "Ciudadano peruano.");
            UsuarioDonante = c4.Model.AddPerson("Usuario Donante", "Ciudadano peruano.");
            Admin = c4.Model.AddPerson("Admin", "Usario Admin.");
            Hospital = c4.Model.AddPerson("Hospital", "Centro de Salud");
        }

		private void AddSoftwareSystems()
		{
			MonitoringSystem = c4.Model.AddSoftwareSystem("Registro Nacional de Banco de Sangre", "Permite el registro y búsqueda de donantes de sangre en nuestro país");
			GoogleMaps = c4.Model.AddSoftwareSystem("Google Maps", "Plataforma que ofrece una REST API de información geo referencial.");
            GoogleCalendar = c4.Model.AddSoftwareSystem("Google Calendar", "Plataforma que ofrece servicio de calendario");
            DonationSystem = c4.Model.AddSoftwareSystem("Donation System", "Permite mostrar la información de diferentes donantes a nuestro sistema");
        }

		private void AddRelationships() {
            UsuarioReceptor.Uses(MonitoringSystem, "Realiza consultas para estar al tanto de la recopilación de datos de diferentes donantes");
            UsuarioDonante.Uses(MonitoringSystem, "Realiza consultas para poder visualizar la fecha de su cita de donación de sangre");
            Admin.Uses(MonitoringSystem, "Realiza consultas para mantenerse al tanto sobre el registro de donantes y solicitudes de donatarios en el Perú");
            Hospital.Uses(MonitoringSystem, "Realiza consultas para mantenerse al tanto de las citas programadas");
            
			MonitoringSystem.Uses(GoogleMaps, "Usa la API de google maps");
            MonitoringSystem.Uses(GoogleCalendar, "Usa la API de google calendar");
            MonitoringSystem.Uses(DonationSystem, "Consulta informacion de Donantes");
        }

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;
			
			styles.Add(new ElementStyle(nameof(UsuarioReceptor)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
			styles.Add(new ElementStyle(nameof(Admin)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle(nameof(UsuarioDonante)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle(nameof(Hospital)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            
			styles.Add(new ElementStyle(nameof(MonitoringSystem)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(GoogleMaps)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle(nameof(GoogleCalendar)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle(nameof(DonationSystem)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
        }

		private void SetTags()
		{
			Admin.AddTags(nameof(Admin));
            UsuarioReceptor.AddTags(nameof(UsuarioReceptor));
            UsuarioDonante.AddTags(nameof(UsuarioDonante));
            Hospital.AddTags(nameof(Hospital));

            MonitoringSystem.AddTags(nameof(MonitoringSystem));
			GoogleMaps.AddTags(nameof(GoogleMaps));
			GoogleCalendar.AddTags(nameof(GoogleCalendar));
            DonationSystem.AddTags(nameof(DonationSystem));
        }

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(MonitoringSystem, "Contexto", "Diagrama de Contexto");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
		}
	}
}