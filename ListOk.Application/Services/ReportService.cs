using ListOk.Core.Interfaces;
using System.Text;

public class ReportService
{
    private readonly IBoardService _boardService;

    public ReportService(IBoardService boardService)
    {
        _boardService = boardService;
    }

    public async Task<byte[]> GenerateBoardReportAsync(Guid boardId)
    {
        var board = await _boardService.GetBoardByIdAsync(boardId);
        if (board == null)
            throw new KeyNotFoundException("Board not found");

        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);

        // Записываем информацию о доске
        streamWriter.WriteLine("Board Report");
        streamWriter.WriteLine($"Board Title,{EscapeCsvField(board.Title)}");
        streamWriter.WriteLine($"Description,{EscapeCsvField(board.Description)}");
        streamWriter.WriteLine();

        // Для каждой колонки создаем свою секцию с карточками
        foreach (var column in board.Columns)
        {
            streamWriter.WriteLine($"Column: {EscapeCsvField(column.Title)}");
            streamWriter.WriteLine("Title,Description,Deadline,Status");

            foreach (var card in column.Cards)
            {
                string title = EscapeCsvField(card.Title);
                string description = EscapeCsvField(card.Description);
                string deadline = card.DateDeadline?.ToString("yyyy-MM-dd") ?? "";
                string status = card.Status.ToString();

                streamWriter.WriteLine($"{title},{description},{deadline},{status}");
            }

            streamWriter.WriteLine();
        }

        streamWriter.Flush();
        return memoryStream.ToArray();
    }

    // Вспомогательный метод для экранирования полей CSV
    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
            return string.Empty;

        // Экранирование специальных символов в CSV
        bool needsQuotes = field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n");

        if (needsQuotes)
        {
            // Заменяем кавычки на двойные кавычки и оборачиваем все в кавычки
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }

        return field;
    }
}