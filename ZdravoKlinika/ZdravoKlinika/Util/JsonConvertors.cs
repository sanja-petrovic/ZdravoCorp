using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZdravoKlinika.Model;

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
    public class PatientConverter : JsonConverter<Patient>
    {
        public override Patient Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                Patient.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            Patient patient,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(patient.GetPatientId() + "," + patient.GetPatientType());
    }
    public class RoomConverter : JsonConverter<Room>
    {
        public override Room Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                Room.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            Room room,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(room.RoomId);
    }
}
