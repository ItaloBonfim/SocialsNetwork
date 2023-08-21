using SocialsNetwork.Interfaces.Socials.Business;

namespace SocialsNetwork.Business.Socials
{
    public class Publications : IPublications
    {
        public List<IQueryable> BuscarPublicacoesRecentes()
        {
            throw new NotImplementedException();
            /*
             * O objeto precisa ter 
             * Os dados da publicação
             * Os dados do autor da publicação
             * Total de Comentarios (Se possivel associados com a tabela de amigos)
             * Toral de reacoes (Se possivel associados com a tabela de amigos)
             * Precisa Retornar um total de 24 resultados e ser capaz de buscar para além do que já foi pesquisado (paginação)
             * 
             */
        }
    }
}
