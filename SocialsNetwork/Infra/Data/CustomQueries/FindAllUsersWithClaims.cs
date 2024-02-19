﻿using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Class;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindAllUsersWithClaims
    {
        private readonly IConfiguration Configuration;
        public FindAllUsersWithClaims(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<UserResponseString> Execute(string userLogged,int page, int rows)
        {
            /*
            * Aqui é utilizado a lib Dapper
            * é necessario configurar uma nova conexao de banco para utilizar
            * O resultado da consulta é convertido para o Response endpoint e o campos devem ser nomeados
            * igualmente
            * é possivel utiliza paginação mais completa nesse metodo
            * é mais performatico na maioria dos casos se comparado ao EF
            */
            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"SELECT
                        aspUsers.Id as Id,
                        aspUsers.UserName as UserName,
                        aspUsers.Email as Email,
                        aspUsers.AvatarURL as avatarURL,
                        aspClaim.ClaimValue as 'Name'
                        FROM AspNetUsers as aspUsers
                        INNER JOIN AspNetUserClaims as aspClaim ON (aspUsers.Id = aspClaim.UserId)
                        WHERE 
                        aspUsers.Id <> @userLogged
                        AND aspClaim.ClaimType = 'name'
                        GROUP BY 
                        aspUsers.Id, aspUsers.UserName, 
                        aspUsers.Email, aspUsers.AvatarURL, 
                        aspClaim.ClaimType,
                        aspClaim.ClaimValue
                        ORDER BY aspClaim.ClaimType
                        OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<UserResponseString>(query, new {userLogged ,page, rows });
        }

        public IEnumerable<UserResponseString> FindUserById(string userLogged)
        {
            /*
            * Aqui é utilizado a lib Dapper
            * é necessario configurar uma nova conexao de banco para utilizar
            * O resultado da consulta é convertido para o Response endpoint e o campos devem ser nomeados
            * igualmente
            * é possivel utiliza paginação mais completa nesse metodo
            * é mais performatico na maioria dos casos se comparado ao EF
            */
            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"SELECT
                        aspUsers.Id as Id,
                        aspUsers.UserName as UserName,
                        aspUsers.Email as Email,
                        aspUsers.AvatarURL as avatarURL,
                        aspClaim.ClaimValue as 'Name'
                        FROM AspNetUsers as aspUsers
                        INNER JOIN AspNetUserClaims as aspClaim ON (aspUsers.Id = aspClaim.UserId)
                        WHERE 
                        aspUsers.Id = @userLogged
                        AND aspClaim.ClaimType = 'name'
                        GROUP BY 
                        aspUsers.Id, aspUsers.UserName, 
                        aspUsers.Email, aspUsers.AvatarURL,
                        aspClaim.ClaimType,
                        aspClaim.ClaimValue
                        ORDER BY aspClaim.ClaimType";
            return data.Query<UserResponseString>(query, new { userLogged });
        }
    }
}
