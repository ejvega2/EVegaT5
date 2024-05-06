using EVegaT5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVegaT5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage  {get; set;}
        
        private void Init()
        {
            if (conn is not null)
                return;
                    conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Se requiere un nombre");
                Persona persona = new() {Name = name};
                result = conn.Insert(persona);
                StatusMessage = string.Format("Nombre de la persona ingresado",result,name);
            }catch (Exception ex)
            {
                StatusMessage = string.Format("Error no se ingreso", name, ex.Message);
            }
        }

        public List<Persona> getAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error en listar", ex.Message);
            }
            return new List<Persona>();
        }

        public void EditPerson(int id, string newName)
        {
            try
            {
                Init();
                Persona persona = conn.Table<Persona>().FirstOrDefault(p => p.Id == id);
                if (persona != null)
                {
                    persona.Name = newName;
                    conn.Update(persona);
                    StatusMessage = $"Persona con ID {id} actualizada con éxito.";
                }
                else
                {
                    StatusMessage = $"Persona con ID {id} no encontrada.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al actualizar persona: {ex.Message}";
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                Persona persona = conn.Table<Persona>().FirstOrDefault(p => p.Id == id);
                if (persona != null)
                {
                    conn.Delete(persona);
                    StatusMessage = $"Persona con ID {id} eliminada con éxito.";
                }
                else
                {
                    StatusMessage = $"Persona con ID {id} no encontrada.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar persona: {ex.Message}";
            }
        }
    }
}
