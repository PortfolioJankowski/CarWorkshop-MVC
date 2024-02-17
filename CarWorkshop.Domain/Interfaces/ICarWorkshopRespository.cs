using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Domain.Interfaces
{
    public interface ICarWorkshopRespository
    {
        Task Create(Domain.Entities.CarWorkshop carWorkshop);
        //metoda zwracająca Car Workshop a przyjmująca nazwe tego workshopu
        Task<Domain.Entities.CarWorkshop?> GetByName(string name);
    }
}
