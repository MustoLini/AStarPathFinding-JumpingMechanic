# A* AlgoRithms with Jumping

### BackStory Of The Jump Mechanic
___________________________________________________

I picked first going trough a maze. But there was to much resources online so that topic got denied. But then I decided to go with a Jumping mechaic where you should be able to Jump in the maze and find the fastest way though it. On my side branch I am working on a Jumping mechic where it should be able to jump two times and find the fastest way thougt. In this document you will find the ways I first tried to make my mechanic and what I ended up with. 

### The Story Of The Jump Mechanic
____________________________________________________

While Constucting the A* alogrithm that I found on Sebastian Lague. I found some problem that I hade to solve like one of the problem that I found was that get neighbro method did not work for me. It would not find me the fastets way to the closets neighbor so I had to hard coded his method so it would work. 
But before finding out that was problem, I switched to Iterative Deepening Depth-First-Search. The Iterative Deepening Depth-First-Search I built while I was using the scudocode that was provided from Marc Zaku slides. 

The Iterative Deepening Depth-First-Search hade the same problem as the A* hade so i started to wonder if the get neighbor method was in correct. While I was debuging it I found out that the method does not take two of the neighbors nodes in consideration. Beacuse I hade used wrong < so it never ran.  

Why I picked A* as the algorithm that I would use was because there was much information how to use it and I thought that if I use A* that I could use layers to make it so would only be able to walk though wall 2 times(This would not be jumping when I think back), But that did not register that It would just remove one when coming upp to a wall and deactivate it. This was a pretty dum ide but I though that would be the simple solution to jump/ walk though wall. I wasted a lot of time to try to make it happen but it did not work so that ide was scrapped. 

The second ide was something similar to the first ide but that did not work. The second ide was to make diconary to hold layers and that i could put diffrent cost on diffrent layers. So it would pick if it was worth going round the obstical or jumping/ going though the wall. But that sollution was scarped after the second day on working on it because the would be inconsitensy with the result in finding the fastest way to the point and that would not realy jump between them. 

The third ide that I came upp with and the most promising sollution to make it jump beween nodes would be. That when the node comes up to the layer obsticla that It would check If( the is a Node on the other side && if I have not jumped more then two times). First that I would need to do is to get the Node On the other side. This can maybe be able to use a Dictionary to get all the Node that are out on the field. The checking if they are walkeble/ does not have the layer that are obstical and checking if they could be jumpble. This ide was shut downed because the datastuck that I had made for the grid and pathfinding was not the optimal/shit. 

### The Development Of The Main Jump Mechanic
_______________________________________________
I talked with Marc and he gave me some tips on what I should do and that lead upp to my final ide using states. I remade the PathFindingControlelr also but the layout is almost the same the diffrens is that instead of using Node in openset and closedset, now I am using the PathState. In PathSate I have remade the GetAllNeighbor and made it to a IEnumarble so yield return NewState. In the Pathstate script hold also the optimatation of the patfinding to check that you are not walking on places where it should not walk and jump. 

The Problems that I faced was many but one in particular was that the game crached whenever I put the EndNode or the StartNode/ The End/Start GameObject. This was beacuse the game was checking all possible ways to get to the Point and finding the shortest. This could end upp with 2150 in openset when the it was only 14 nodes/cells from the start to end cell/node.



### Resources That I Worked With

_________________________________

#### Sebastian Lague : https://www.youtube.com/watch?v=-L-WgKMFuhE

His videos helped in making the Grid and Pathfinding and Node. But hade to change somethings to make it work with the game pathfinding and hade to make New PathState class and I hade to make a new A* PathFinding system with new hyristic and hade to change many things in the Node class and Grid Class.

#### Marc Zaku pseudocode for the PathState: 

``CurrentState:
   position = x,y
   canJump = true
GetAllNeighbors(currentState){
     foreach(node in left,right,up,down){
         if(node.isWall && currentState.canJump)
              return new Node{node, canJump = false)
         else if(!node.isWall)
              return new Node{node, canJump = currentState.canJump};
     }
}``

