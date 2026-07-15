using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParqueDiversiones
{
    // Clase que representa a una persona en la cola
    public class Persona
    {
        public string Nombre { get; set; }
        public int Id { get; set; }

        public Persona(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return $"[ID: {Id:D2}] - {Nombre}";
        }
    }

    // Clase que gestiona la atracción
    public class Atraccion
    {
        private Queue<Persona> colaEspera;
        private List<Persona> asientosAsignados;
        private const int CapacidadMaxima = 30;

        public Atraccion()
        {
            colaEspera = new Queue<Persona>();
            asientosAsignados = new List<Persona>();
        }

        // Proceso: Registrar persona en la cola (Enqueue)
        public void RegistrarPersona(string nombre)
        {
            if (colaEspera.Count + asientosAsignados.Count < CapacidadMaxima)
            {
                int nuevoId = colaEspera.Count + asientosAsignados.Count + 1;
                Persona nuevaPersona = new Persona(nuevoId, nombre);
                colaEspera.Enqueue(nuevaPersona);
                Console.WriteLine($"✅ {nombre} ha sido registrado en la cola de espera.");
            }
            else
            {
                Console.WriteLine("❌ Lo sentimos, todos los asientos (30) ya han sido reservados o están en cola.");
            }
        }

        // Proceso: Asignar asientos (Dequeue)
        public void ProcesarEntrada()
        {
            if (colaEspera.Count == 0)
            {
                Console.WriteLine("⚠️ No hay personas en la cola para procesar.");
                return;
            }

            Console.WriteLine("\n--- Iniciando abordaje a la atracción ---");
            while (colaEspera.Count > 0)
            {
                Persona p = colaEspera.Dequeue();
                asientosAsignados.Add(p);
                Console.WriteLine($"🎟️ Asiento asignado a: {p.Nombre} (Orden de llegada respetado)");
            }
            Console.WriteLine("--- Proceso de abordaje completado ---\n");
        }

        // Reportería: Visualizar estado de la estructura
        public void MostrarEstado()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("      REPORTE DE LA ATRACCIÓN");
            Console.WriteLine("========================================");
            
            Console.WriteLine($"\nAsientos ocupados/reservados: {asientosAsignados.Count + colaEspera.Count} / {CapacidadMaxima}");
            
            Console.WriteLine("\n--- Personas ya en la atracción (Asignadas) ---");
            if (asientosAsignados.Any())
                asientosAsignados.ForEach(p => Console.WriteLine(p));
            else
                Console.WriteLine("Ninguna persona ha abordado aún.");

            Console.WriteLine("\n--- Personas en línea de espera (Cola FIFO) ---");
            if (colaEspera.Any())
            {
                foreach (var p in colaEspera)
                {
                    Console.WriteLine(p);
                }
            }
            else
            {
                Console.WriteLine("La cola está vacía.");
            }
            Console.WriteLine("========================================\n");
        }

        public int ObtenerTotal() => asientosAsignados.Count + colaEspera.Count;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Atraccion montanaRusa = new Atraccion();
            Stopwatch sw = new Stopwatch();

            Console.WriteLine("SISTEMA DE GESTIÓN DE ATRACCIONES - PARQUE DE DIVERSIONES");
            
            // Simulación de llegada de personas
            string[] nombresSimulados = { 
                "Juan Perez", "Maria Garcia", "Carlos Lopez", "Ana Martinez", 
                "Luis Rodriguez", "Elena Gomez", "Pedro Sanchez", "Laura Diaz",
                "Diego Torres", "Sofia Ruiz" 
            };

            sw.Start();
            
            Console.WriteLine("\n1. REGISTRANDO PERSONAS EN LA COLA...");
            foreach (var nombre in nombresSimulados)
            {
                montanaRusa.RegistrarPersona(nombre);
            }

            montanaRusa.MostrarEstado();

            Console.WriteLine("2. PROCESANDO EL ABORDAJE (ORDEN FIFO)...");
            montanaRusa.ProcesarEntrada();

            montanaRusa.MostrarEstado();

            sw.Stop();

            Console.WriteLine($"\nANÁLISIS DE RENDIMIENTO:");
            Console.WriteLine($"- Tiempo de ejecución de la simulación: {sw.Elapsed.TotalMilliseconds:F4} ms");
            Console.WriteLine($"- Estructura utilizada: Queue<T> (Cola)");
            Console.WriteLine($"- Complejidad Enqueue/Dequeue: O(1)");
            
            Console.WriteLine("\nEjecución finalizada.");
        }
    }
}
