sum_inv = []
summe = 0
max1 = 0
max2 = 0
max3 =0

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
    if calories > max1:
        max1 = calories
elf_count = (sum_inv.index(max1) + 1)
print(f"Elf number {elf_count} carries the most calories with {max1}")

for calories in sum_inv:
    if calories > max2 and calories < max1:
        max2 = calories
elf_count2 = (sum_inv.index(max2) + 1)
print(f"Elf number {elf_count2} carries the second most calories with {max2}")

for calories in sum_inv:
    if calories > max3 and calories < max2 and calories <max1:
        max3 = calories
elf_count3 = (sum_inv.index(max3) + 1)
print(f"Elf number {elf_count3} carries the thrid most calories with {max3}")

total = max1 + max2 + max3
print(total)
