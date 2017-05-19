using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

/// <summary>
/// Descripción breve de Medical
/// </summary>
public class Medical
{
	public Medical()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    /// <summary>Datos de la clase.</summary>
    //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Clase", Order = 0)]
    public Clase Clase { get; set; }

    /// <summary>Lista de usuario pertenecientes a la clase.</summary>
    //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Usuario", Order = 1)]
    public List<Usuario> Usuario { get; set; }
}

[Serializable]
    //[KnownType(typeof(Clase))]
    //[DataContract(Name = @"Clase", IsReference = false)]
    public class Clase
    {
        /// <summary>Nombre de la clase.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Nombre", Order = 0)]
        public string Nombre { get; set; }

        /// <summary>Fecha pautada para la clase.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Fecha", Order = 1)]
        public string Fecha { get; set; }

        /// <summary>Hora de inicio de la clase.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Hora", Order = 2)]
        public string Hora { get; set; }

        /// <summary>Intervalo de tiempo en minutos que tarda el trabajador en tomarse las muestras en un sensor.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Intervalo", Order = 3)]
        public int Intervalo { get; set; }

        /// <summary>Cantidad de sensores que serán usados para la clase.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Sensor", Order = 4)]
        public string Sensor { get; set; }

        /// <summary>Nombre de la estacion utilizada.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"SensorNombre", Order = 4)]
        public string SensorNombre { get; set; }
    }

    [Serializable]
    //[KnownType(typeof(Usuario))]
    //[DataContract(Name = @"Usuario", IsReference = false)]
    public class Usuario
    {
        /// <summary>Cédula del trabajador.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Cedula", Order = 0)]
        public string Cedula { get; set; }

        /// <summary>Nombre del trabajador.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Nombre", Order = 1)]
        public string Nombre { get; set; }

        /// <summary>Turno solicitado por el trabajador.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Turno", Order = 2)]
        public int Turno { get; set; }

        /// <summary>Hora de inicio de la clase.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"HoraInicial", Order = 3)]
        public string HoraInicial { get; set; }

        /// <summary>Hora final de la clase.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"HoraFinal", Order = 4)]
        public string HoraFinal { get; set; }

        /// <summary>Sexo.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"Sexo", Order = 4)]
        public string Sexo { get; set; }

        /// <summary>Fecha de Nacimiento.</summary>
        //[DataMember(EmitDefaultValue = false, IsRequired = true, Name = @"FechaNacimiento", Order = 4)]
        public string FechaNacimiento { get; set; }
    }