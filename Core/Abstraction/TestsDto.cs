namespace Core.Abstraction
{
    public record TestDto(Guid Id, string Title, Guid Users_id);
    public record RoomDto(Guid Id, string ExitKeyWord, Guid Test_id);
    public record QuestionDto(Guid Id, string Text, string ExitKeyLetter, Guid Room_id);

    public record CreateTestDto(string Title);
    public record CreateRoomDto(string ExitKeyWord);
    public record CreateQuestionDto(string Text, string ExitKeyLetter);

    public record CreateFullTestDto(
        string Title,
        List<CreateRoomWithQuestionsDto> Rooms
    );

    public record CreateRoomWithQuestionsDto(
        string ExitKeyWord,
        List<CreateQuestionDto> Questions
    );

    public record FullTestDto(
        Guid Id,
        string Title,
        Guid Users_id,
        List<RoomWithQuestionsDto> Rooms
    );

    public record RoomWithQuestionsDto(
        Guid Id,
        string ExitKeyWord,
        Guid Test_id,
        List<QuestionDto> Questions
    );

    public record CheckAnswerDto(
        Guid RoomId,
        List<UserAnswerDto> Answers
    );

    public record UserAnswerDto(
        Guid QuestionId,
        string Answer
    );
}