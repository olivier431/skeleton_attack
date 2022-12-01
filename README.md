# Skeleton Attack
Olivier Lefebvre
## Introduction
Ce jeu est un jeu de plateforme 2D où le héros devra se défendre contre un ordre de squelette dans deux niveaux différents. Un étant complètement dans le noir sauf un rond à l'entour du joueur, afin que celui-ci puisse voir les ennemies seulement dans ce champ et l'autre ayant une "spawner" de monstres dans le haut du niveau, pour rencontrer des monstres tout au long du parcours du joueur. Le joueur devra tuer des squelettes afin d'avoir **un score égal ou supérieur a 5** afin de gagner la partie.

## Le Parallax Background ou Défilement de parallaxe
ce concept intégré a Godot permet la gestion de l'arrière plan afin que plusieurs couches soient affichées et que celles-ci avancent a différente vitesse pendant la partie. Ce concept permet également de créer un illusion de profondeur à l'arrière-plan.

<table>
    <tr>
        <td>
            <figure>
                <img src="Asset/Concept/parallax1.png"/>
                <figcaption>liste des couchent utiliser dans le parallax. Ces couches sont des ParallaxLayer auxquelles sont attachées un sprite</figcaption>
            </figure>
        </td>  
        <td>  
            <figure>
                <img src="Asset/Concept/parallax2.png"/>
                <figcaption>parallax final avec toutes les couches supperposées</figcaption>
            </figure>
        </td>
    </tr>
</table>

Pour effectuer ce concept, je me suis inspiré des notes de cours de notre professeur Nicolas : https://cshawi-my.sharepoint.com/:p:/g/personal/nbourre_cshawi_ca/EaAmN5-Ik35OpyZFpZZq5u8BdAaPWGQkXiLsZ2-wIhElhQ?e=UtglSq

## State Machine ou La machine à états finis
Ce concept permet d'avoir un code beaucoup plus facile à lire sans avoir des "if" à répétition. Ce concept nous permet de gérer facilement les états de notre personnage qui sont au préalable dans un "enum". Par exemple, lorsque nous voulons que notre personnage saute ou cours tout juste après avoir attaqué, il suffit simplement de changer l'état pour que celle-ci ce change dans la switch.
<table>
    <tr>
        <td>
            <figure>
                <img src="Asset/Concept/State_Machine.PNG"/>
                <figcaption>Switch de state ainsi que le changement d'état affiché en console</figcaption>
            </figure>
        </td>  
    </tr>
</table>

Pour effectuer ce concept, je me suis inspiré des notes de cours de notre professeur Nicolas : https://docs.google.com/presentation/d/1spE-ETEnnZquTMeOgCaZRPmHi46JVmVQgUN-XiN3pdg/edit#slide=id.g18e9bb5ccf_0_62
