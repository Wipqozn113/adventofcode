windows = []
count = 0
n = 0
curr = 0
with open('p1.in') as file:
    for line in file:
        depth = int(line.strip())
        windows.append(depth)
        if(n > 0):
            i = n - 1
            while(i > -1 and i > (n - 3)):
                windows[i] += depth
                i -= 1
            
        if(n > 2):
            if(windows[curr] < windows[curr+1]):
                count += 1
            curr += 1
        n += 1

print(count)