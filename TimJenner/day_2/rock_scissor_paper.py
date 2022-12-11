'''
a_rock
b_paper
c_scissor
---
            score:
x_rock          1
y_paper         2
z_scissor       3

lose = 0 
draw = 3 
win = 6 

'''
input_me = " "
input_elf = " "
input_txt = open("input.txt")
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

    if input_me == "x" and input_elf == "a":
        total_score += draw + x_rock
    elif input_me == "x" and input_elf == "b":
        total_score += lose + x_rock
    elif input_me == "x" and input_elf == "c":
        total_score += win + x_rock

    elif input_me == "y" and input_elf == "a":
        total_score += win + y_paper
    elif input_me == "y" and input_elf == "b":
        total_score += draw + y_paper
    elif input_me == "y" and input_elf == "c":
        total_score += lose + y_paper

    elif input_me == "z" and input_elf == "a":
        total_score += lose + z_scissor
    elif input_me == "z" and input_elf == "b":
        total_score += win + z_scissor
    elif input_me == "z" and input_elf == "c":
        total_score += draw + z_scissor

print(f"Score: {total_score}")
