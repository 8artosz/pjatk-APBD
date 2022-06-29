using cw4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Services
{
    public interface IDbService
    {
        public bool CheckAnimalById(int idAnimal);
        public List<Animal> GetAnimal(string orderBy);
        public string PutAnimal(int idAnimal, Animal animal);
        public string PostAnimal(Animal animal);
        public string DeleteAnimal(int idAnimal);


    }
}
