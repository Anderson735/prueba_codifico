using prueba_codifico.DTO.Interfaces.Production;

namespace prueba_codifico.DataAccess.Repository.DML.Production
{
    public class SuppliersDMLRepository : ISuppliers
    {
        private readonly string _connectionString;

        public SuppliersDMLRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


    }
}
