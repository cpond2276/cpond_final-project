Overview and Reflection:
My initial goal was to get a working video game of some kind. That game ended up being tic-tac-toe. There was a lot I wanted to accomplish with this project. I needed to be able to make the board, update the board on both the users and computers turn, then I needed to make it an actual challenge to win the game by adding real strategy to the way the computer played. That was both the hardest and most rewarding part of the project.

This project was very interesting to me because as I thought about it more and worked my way through it I found way more little details and roadblocks that I would have thought. It was really interesting developing a simple yet effective strategy for the computer. It was also fun adding little details to the flow of the game to make it feel smoother and more natural. Clearing the console at the right time and adding little pauses or messages to give it some personality was something I hadn't originally planned but I feel it helps fulfill its function as game rather than utilitarian code.

I was able to get a functioning game of tic-tac-toe. After an intro the player takes the first turn by inputting a number that corresponds to place on the board they want to do. After that the computer's turn consists of 4 simple checks, check to see if it can win, check to see if the user can win on the next turn, check to see if the middle spot is open, then if it doesn't do any of those it takes a random spot. I also set it up so that with additional time it is fairly simple to add more steps to the computer strategy or even switch it so that the computer could go first rather than the user.

One of the things that I knew in theory was how useful methods were, but this project really helped me understand how great they are. They give a lot more flexibility to the code, and making the methods then adding them together in the game loop felt like a simpler process rather than trying to make it all out of a bunch of unorganized loops. I gained more technical knowledge about reading from and using other files as well as the use and limits of 2d arrays. The biggest thing I learned though it about the importance of preperation and planning. I think the most important thing I learned was the importance of really planning out bigger projects. This was the biggest coding project I've worked on, but I think it is also the one that came out the cleanest. I think that is because I planned and structured everything much more carefully than previous projects.


Pseudo Code

run test
print welcome message
begin game loop(is game going true or false)
    print board (create method for this, will do a lot of times)
    ask player for input (create method for this)
    update board from input
    check for win (create method for this)
        if yes display win and add to win total
    check for tie (create method for this)
        if yes display tie and add to tie total
    computer turn(create method)(if else statements in this order)
        if win on this turn possible take square
        if player win possible on next turn take it
        if middle is open take is
        take random spot if the rest weren't true
    update board from computer turn
    check for win
        if yes display win and add to win total
    check for tie
        if yes display tie and add to tie total
when game ends ask player to play again
 if yes 
    begin game loop again
 if no
    print wins and losses
    goodbye
    
  
  
  
