using System;
using System.Text.Json;
using System.Text.Json.Serialization;

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