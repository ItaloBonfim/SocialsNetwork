namespace SocialsNetwork.DTO.Socials
{
    public record Comments();
    public record CommentsRequest(Guid Publication, string Comment, string ImageURL, string MidiaURL);
    public record CommentsResponse(string User, string AvatarURL, string name, Guid Comment, DateTime CreatedOn, string Text, string ImageURL, string MidiaURL, Guid Publication, DateTime UpdatedOn, int QtdAnswers, int QtdReactions);
    public record CommentsUpdate();

}

/*
 *            aspUsers.Id AS 'User',
            aspUsers.AvatarURL,
            CMM.Id as 'Comment',
            CMM.CreatedOn,
            CMM.CommentValue as 'Text',
            CMM.ImageURL as 'ImageURL',
            CMM.MidiaURL 'MidiaURL',
            CMM.PublicationId as 'Publication',
            CMM.UpdatedOn,
            
            (SELECT COUNT(Id) FROM SubComments AS SUB
                WHERE SUB.CommentId = CMM.Id) AS 'QtdAnswers',

            (SELECT COUNT(Id) FROM CommentReactions AS CR
                WHERE CR.CommentId = CMM.Id) AS 'QtdReactions'  
 */
