sum_inv = []
summe = 0
max = 0

input_txt = open("input.txt")
inventory = input_txt.readlines()

for counter in range(len(inventory)):
    if inventory[counter] == "\n":
        sum_inv.append(summe)
        summe = 0
    elif counter == (len(inventory)-1): 
        summe += int(inventory[counter])
        sum_inv.append(summe)
    else:
        summe += int(inventory[counter])
print(sum_inv)

for calories in sum_inv:
    if calories > max:
        max = calories
elf_count = (sum_inv.index(max) + 1)
print(f"Elf number {elf_count} carries the most calories with {max}")
