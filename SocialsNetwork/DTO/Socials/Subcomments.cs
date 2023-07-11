namespace SocialsNetwork.DTO.Socials
{
    public record Subcomments ();
    public record newSubcomment (Guid CommentId, string Comment, string ImageURL, string MidiaURL);
    public record responseSubcomment(string User, string Name, string AvatarURL, string pCommentId, string pCommentValue, string pCommentImageURL, string pCommentMidiaURL,
                                    string pCreatedOn, string pUpdatedOn, string sCommentId, string sCommentValue, string sImageURL, string sMidiaURL, string sCreatedOn, string sUpdateOn);
    
   
}
