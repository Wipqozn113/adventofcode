prev = None
count = 0
with open('p1.in') as file:
    for line in file:
        depth = int(line.strip())
        if((prev is not None) and (depth > prev)):
            count += 1
        prev = depth

print(count)

