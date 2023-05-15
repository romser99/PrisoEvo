using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disply_Evo
{
    public class PopService{
    

        // interaction 1 a 1 en fonction du type
        public List<int> interaction1to1(Population popCible, Population popRegarde){
            int typeCible = popCible.type;
            int typeRegarde = popRegarde.type;
            bool choixCible = false;
            bool choixRegarde = false;
            List<int> points = new List<int> {0,0,0,0};

            // population "altruiste" coopère toujours
            if (typeCible == 1){
                choixCible = true;
                if (typeRegarde == 1){
                    choixRegarde = true;
                }
                else if (typeRegarde == 2){
                    choixRegarde = false;   
                }
                else if (typeRegarde == 3){
                    choixRegarde = false;  
                }
                else if (typeRegarde == 4){
                    if (popRegarde.memoire == 0){
                        choixRegarde = true;
                    }
                    else {
                        choixRegarde = false;
                    }
                }
            }
            // population "individualiste" trahit toujours
            else if (typeCible == 2){
                choixCible = false;
                if (typeRegarde == 1){
                    choixRegarde = true;
                }
                else if (typeRegarde == 2){
                    choixRegarde = false;   
                }
                else if (typeRegarde == 3){
                    choixRegarde = false;  
                }
                else if (typeRegarde == 4){
                    if (popRegarde.memoire == 0){
                        choixRegarde = true;
                    }
                    else {
                        choixRegarde = false;
                    }
                }    
            }
            // population "familial" ne trahit pas ses congénères
            else if (typeCible == 3){
                
                if (typeRegarde == 1){
                    choixRegarde = true;
                    choixCible = false;
                }
                else if (typeRegarde == 2){
                    choixRegarde = false;
                    choixCible = false;   
                }
                else if (typeRegarde == 3){
                    choixRegarde = true;
                    choixCible = true;  
                }
                else if (typeRegarde == 4){
                    if (popRegarde.memoire == 0){
                        choixRegarde = true;
                        choixCible = false;
                    }
                    else {
                        choixRegarde = false;
                        choixCible = false;
                    }
                }    
            }
            // population "rancunier" : peu importe avec qui a été sa dernière interaction il va la recopier ce qu'il a subi
            else if (typeCible == 4){
                if (popCible.memoire == 0){
                    choixCible = true;
                }
                else {
                    choixCible = false;
                }
                
                if (typeRegarde == 1){
                    choixRegarde = true;
                   
                }
                else if (typeRegarde == 2){
                    choixRegarde = false;
                       
                }
                else if (typeRegarde == 4){
                    if (popRegarde.memoire == 0){
                        choixRegarde = true;
                        
                    }
                    else {
                        choixRegarde = false;
                        
                    }
                }    
            }
            if (typeCible == 4){
                if (choixRegarde == false){
                    points[2] = 1;
                }
                else {
                    points[2] = 0;
                }
            }
            if (typeRegarde == 4){
                if (choixCible == false){
                    points[3] = 1;
                }
                else {
                    points[3] = 0;
                }
            }

            // Calcul des points pour cette interaction
            if (choixCible == choixRegarde){
                if (choixCible == true){
                    points[0] = 2;
                    points[1] = 2;
                }
                else {
                    points[0] = 5;
                    points[1] = 5;
                }                
            }
            else {
                if (choixCible == true){
                    points[0] = 10;
                    points[1] = 0;
                }
                else {
                    points[0] = 0;
                    points[1] = 10;

                }
            }

            return points;

         


    
    
        }
    }
}