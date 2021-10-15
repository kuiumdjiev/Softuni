using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRandomList
{
  public  class RandomList:List<string>
    {
        private Random random;
        public RandomList()
        {
            random = new Random();
        }
        public void Add(string msg)
        {
            base.Add(msg);
        }
        public string RandomString()
        {

            if (this.Count==0)
            {
                return "";
            }
            int index = random.Next(0, this.Count);
            string item = this[index];
            this.RemoveAt(index);
            return item;
        }
    }
}
