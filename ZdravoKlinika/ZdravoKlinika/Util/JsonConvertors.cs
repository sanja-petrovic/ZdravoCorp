using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonConverters 
{
    public class MedicalRecordConverter : JsonConverter<MedicalRecord>
    {
        public override MedicalRecord Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                MedicalRecord.Parse(reader.GetString()!);

        public override void Write(
            Utf8JsonWriter writer,
            MedicalRecord medical,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(medical.MedicalRecordId);
    }

    public class DoctorConverter : JsonConverter<Doctor>
    {
        public override Doctor Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                Doctor.Parse(reader.GetString()!);

        public override void Write(
            Utf8JsonWriter writer,
            Doctor doctor,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(doctor.PersonalId);
    }
    public class RegisteredPatientConverter : JsonConverter<RegisteredPatient>
    {
        public override RegisteredPatient Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                RegisteredPatient.Parse(reader.GetString()!);

        public override void Write(
            Utf8JsonWriter writer,
            RegisteredPatient patient,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(patient.PersonalId);
    }
}
