'''
a_rock
b_paper
c_scissor
---
            score:
x_rock          1
y_paper         2
z_scissor       3

x = lose
y = draw
z = win

lose = 0 
draw = 3 
win = 6 

'''
input_me = " "
input_elf = " "
input_txt = open("test_input.txt")
inventory = input_txt.readlines()

total_score = 0
x_rock= 1
y_paper= 2
z_scissor= 3
lose = 0 
draw = 3 
win = 6 

for line in inventory:
    input_elf = line[0].lower()
    input_me = line[2].lower()

    if input_elf == "a" and input_me == "x":
        total_score += lose + z_scissor
    elif input_elf == "a" and input_me == "y":
        total_score += draw + x_rock
    elif input_elf == "a" and input_me == "z":
        total_score += win + y_paper

    elif input_elf == "b" and input_me == "x":
        total_score += lose + x_rock
    elif input_elf == "b" and input_me == "y":
        total_score += draw + y_paper
    elif input_elf == "b" and input_me == "z":
        total_score += win + z_scissor

    elif input_elf == "c" and input_me == "x":
        total_score += lose + y_paper
    elif input_elf == "c" and input_me == "y":
        total_score += draw + z_scissor
    elif input_elf == "c" and input_me == "z":
        total_score += win + x_rock
    
print(f"Score: {total_score}")
