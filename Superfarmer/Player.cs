using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superfarmer
{
    public class Player
    {
        public int name = 0;
        public int rabbit;
        public int sheep;
        public int pig;
        public int cow;
        public int horse;
        public int smallDog;
        public int largeDog;
        public Player() { 
            rabbit = 1;
            sheep = 0;
            pig = 0;
            cow = 0;
            horse = 0;
            smallDog = 0;
            largeDog = 0;
            name += 1;
        }


        public override string ToString()
        {
            return($"Rabbits: {rabbit}\nSheeps: {sheep}\nPigs: {pig}\nCows: {cow}\nHorses: {horse}\nSmall Dogs: {smallDog}\nLarge Dogs: {largeDog}");
        }
    }
}
