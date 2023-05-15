using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disply_Evo
{
    public class TableService
    {
        private PopService popService = new PopService();

        //Attribution des points dans un tableau rond, les une population interagis a droite et en dessous d'elle meme
        //Comme les interactions sont symétriques chaque population a bien interagi avec tout ses voisins
        public Population[,] tableinteraction(Population[,] init)
        {
            Population[,] newarray = init;
            int row = init.GetLength(0);
            int col = init.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Population popCible = init[i, j];
                    Population pop1 = new Population();
                    Population pop2 = new Population();
                    List<int> points = new List<int>();
                    // Cas bord inférieur droit
                    if (i == row - 1 && j == col - 1)
                    {
                        pop1 = init[0, j];
                        pop2 = init[i, 0];

                    }
                    // Cas Bas de tableau
                    else if (i == row - 1)
                    {
                        pop1 = init[0, j];
                        pop2 = init[i, j + 1];
                    }
                    // Cas droite de tableau
                    else if (j == col - 1)
                    {
                        pop1 = init[i + 1, j];
                        pop2 = init[i, 0];
                    }
                    //cas général
                    else
                    {
                        pop1 = init[i + 1, j];
                        pop2 = init[i, j + 1];
                    }

                    points = popService.interaction1to1(popCible, pop1);
                    //points
                    newarray[i, j].points += points[0];
                    newarray[pop1.row, pop1.col].points += points[1];
                    //paramètres utiles
                    popCible.memoire = points[2];
                    init[pop1.row, pop1.col].memoire = points[3];
                    points = popService.interaction1to1(popCible, pop2);
                    //points
                    newarray[i, j].points += points[0];
                    newarray[pop2.row, pop2.col].points += points[1];
                    //paramètres utiles
                    init[i,j].memoire = points[2];
                    init[pop2.row, pop2.col].memoire = points[3];

                }
            }

            return newarray;

        }

        public Population[,] evolution(Population[,] generation)
        {
            int rowCount = generation.GetLength(0);
            int colCount = generation.GetLength(1);


            //Passage par une liste pour trier le tableau
            List<Population> newgen = new List<Population>();

            for (int i = 0; i < rowCount; i++)
            {

                for (int j = 0; j < colCount; j++)
                {

                    newgen.Add(generation[i, j]);

                }
            }

            // Critere de triage (points ascendants)
            Comparison<Population> points = (x, y) => x.points.CompareTo(y.points);


            // Triage
            newgen.Sort(points);
            int Lsize = newgen.Count;
            // Combien à tuer/reproduire arrondi au dessous (pour eviter de sortir de la taille du tableau)
            decimal cull = Lsize / 2;
            int intcull = Decimal.ToInt32(Math.Round(cull, 0));

            //On réécrit seulement le type pour eviter de toucher les coordonnées;
            for (int i = 0; i < intcull; i++)
            {
                newgen[i + intcull].type = newgen[i].type;
            }
            //On remplis le tableau en se basant sur les coordonées des populations
            foreach (Population pop in newgen)
            {
                pop.points = 0;
                generation[pop.row, pop.col] = pop;
            }

            return generation;


        }
    }

}