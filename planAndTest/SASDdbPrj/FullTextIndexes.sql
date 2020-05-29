CREATE FULLTEXT INDEX ON [dbo].[article]
    ([articleTitle] LANGUAGE 1033, [articleContent] LANGUAGE 1033)
    KEY INDEX [PK_article]
    ON [catalog1];

