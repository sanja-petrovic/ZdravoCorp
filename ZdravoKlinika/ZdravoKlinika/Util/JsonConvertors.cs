using System;
using System.Collections.Generic;
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
    public class RegisteredUserConverter : JsonConverter<RegisteredUser>
    {
        public override RegisteredUser Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                RegisteredUser.ParseId(reader.GetString()!);

        public override void Write(
           Utf8JsonWriter writer,
           RegisteredUser user,
           JsonSerializerOptions options) =>
               writer.WriteStringValue(user.PersonalId);
    }

    public class CurrentUserConverter : JsonConverter<RegisteredUser>
    {
        public override RegisteredUser Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            RegisteredUser.ParseEmail(reader.GetString()!);

        public override void Write(
           Utf8JsonWriter writer,
           RegisteredUser user,
           JsonSerializerOptions options) =>
               writer.WriteStringValue(user.Email);
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
    public class PatientConverter : JsonConverter<IPatient>
    {
        public override IPatient Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                IPatient.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            IPatient patient,
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

    public class MedicationConverter : JsonConverter<Medication>
    {
        public override Medication Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => Medication.Parse(reader.GetString());


        public override void Write(
            Utf8JsonWriter writer,
            Medication medication,
            JsonSerializerOptions options) => writer.WriteStringValue(medication.MedicationId);
    }

    public class EquipmentConverter : JsonConverter<Equipment>
    {
        public override Equipment Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                Equipment.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            Equipment eq,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(eq.Id + "," + eq.Name + "," + eq.Amount + "," + eq.Expendable);
    }
    
    public class PrescriptionConverter : JsonConverter<Prescription>
    {
        public override Prescription Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => Prescription.Parse(reader.GetInt32());

        public override void Write(
            Utf8JsonWriter writer,
            Prescription prescription,
            JsonSerializerOptions options) => writer.WriteNumberValue(prescription.Id);

    }

}
