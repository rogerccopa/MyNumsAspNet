using MyNumsWeb.Models;
using System.Collections.Generic;

namespace MyNumsWeb.Repository
{
    internal interface IMyNumsRepo
    {
        int AddNewNumbers(int[] numbers);
        IEnumerable<MyNum> GetNumbers();

        void UpdateNum(int id, string note);
    }
}
