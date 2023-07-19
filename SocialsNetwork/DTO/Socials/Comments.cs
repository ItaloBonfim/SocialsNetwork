﻿namespace SocialsNetwork.DTO.Socials
{
    public record Comments();
    public record CommentsRequest(Guid Publication, string Comment, string ImageURL, string MidiaURL);
    public record CommentsResponse(Guid User, string AvatarURL, string Comment, DateTime CreatedOn, string Text, string ImageURL, string MidiaURL, Guid Publication, DateTime UpdateOn, int QtdAnswers, int QtdReactions);
    public record CommentsUpdate();
   


}
