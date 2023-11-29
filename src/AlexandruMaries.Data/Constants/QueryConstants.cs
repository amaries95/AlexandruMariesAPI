namespace AlexandruMaries.Data.Constants;

public static class QueryConstants
{
    public static string GetAllVisibleReferences = """
        SELECT TOP (1000) [Id]
            ,[Summary]
            ,[Author]
            ,[JobTitleAuthor]
            ,[IsVisible]
        FROM [dbo].[Reference]
        WHERE IsVisible = 1
        """;
}