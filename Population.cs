using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disply_Evo
{
    public class Population
    {
        // type de comportement
        public int type { get; set; } = 0;

        // points reçus lors des interactions il faut en avoir le moins
        public int points { get; set; }

        // derniere interaction subie 0 = cooperation 1 = trahison
        public int memoire {get; set; } = 0;


        // Enregistrement des coordonnées
        public int row { get; set; }

        public int col { get; set; }

        public Population(){

        }
        public Population(int Type, int Points)
        {
            type = Type;
            points = Points;
        }

        public Population(int Type, int Points,int Row, int Col)
        {
            type = Type;
            points = Points;
            row = Row;
            col = Col;
        }

       

        
    }
}