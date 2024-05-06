using EVegaT5.Models;

namespace EVegaT5.Views;

public partial class vPersona : ContentPage
{
    private Persona personaSeleccionada; // Variable para almacenar el elemento seleccionado
    public vPersona()
	{
		InitializeComponent();

        listaPersona.SelectionChanged += OnPersonaSelected; // Manejar el evento de selección
    }

    private void OnPersonaSelected(object sender, SelectionChangedEventArgs e)
    {
        personaSeleccionada = e.CurrentSelection.FirstOrDefault() as Persona; // Obtener el elemento seleccionado
    }

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.pRepo.AddNewPerson(txtNombre.Text);
        lblStatus.Text = App.pRepo.StatusMessage;
    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people = App.pRepo.getAllPeople();
        listaPersona.ItemsSource = people;
        lblStatus.Text = App.pRepo.StatusMessage;
        RefrescarLista(); // Actualizar la lista después de insertar
    }

    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        if (personaSeleccionada != null) // Usar la variable para verificar si hay selección
        {
            string nuevoNombre = txtNuevoNombre.Text;
            if (string.IsNullOrEmpty(nuevoNombre))
            {
                DisplayAlert("Editar", "Por favor, ingrese un nuevo nombre.", "OK");
                return;
            }

            App.pRepo.EditPerson(personaSeleccionada.Id, nuevoNombre);

            DisplayAlert("Editado", $"Persona {personaSeleccionada.Name} actualizada.", "OK");

            RefrescarLista(); // Refrescar la lista
        }
        else
        {
            DisplayAlert("Error", "Seleccione una persona para editar.", "OK");
        }
    }

    private void RefrescarLista()
    {
        listaPersona.ItemsSource = App.pRepo.getAllPeople();
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (personaSeleccionada != null)
        {
            App.pRepo.DeletePerson(personaSeleccionada.Id);
            DisplayAlert("Eliminado", $"Persona {personaSeleccionada.Name} eliminada.", "OK");

            RefrescarLista();
        }
        else
        {
            DisplayAlert("Error", "Seleccione una persona para eliminar.", "OK");
        }
    }

}