using MySql.Data.MySqlClient;

var connectionString = "Server=10.131.11.104;Database=Attendance_db;User=root;Password=spongebob;";
var tables = new[]
{
    "AdmissionDetail",
    "Attendance",
    "AttendancePhotograph",
    "Detector",
    "Embedding",
    "EmbeddingType",
    "PhotoType",
    "Student",
    "StudentAttendancePhoto",
    "StudentAttendanceRecord",
    "StudentPhoto",
    "SyncQueue",
    "Teacher"
};

await using var conn = new MySqlConnection(connectionString);
await conn.OpenAsync();

await using (var cmd = new MySqlCommand("SET FOREIGN_KEY_CHECKS=0;", conn))
{
    await cmd.ExecuteNonQueryAsync();
}

foreach (var table in tables)
{
    await using var cmd = new MySqlCommand($"TRUNCATE TABLE {table};", conn);
    await cmd.ExecuteNonQueryAsync();
    Console.WriteLine($"Truncated: {table}");
}

await using (var cmd = new MySqlCommand("SET FOREIGN_KEY_CHECKS=1;", conn))
{
    await cmd.ExecuteNonQueryAsync();
}

await using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM School;", conn))
{
    var count = await cmd.ExecuteScalarAsync();
    Console.WriteLine($"School table kept. Row count: {count}");
}
