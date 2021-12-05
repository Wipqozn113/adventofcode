def most_common(lst):
    zero = lst.count("0")
    one = lst.count("1")

    if(one >= zero):
        return "1"

    return "0"

def least_common(lst):
    zero = lst.count("0")
    one = lst.count("1")

    if(one >= zero):
        return "0"

    return "1"

def create_list(lst):
    items = []
    for l in lst:
        i = 0
        for c in l:
            if(len(items) < i + 1):
                items.append([])
            items[i].append(c)
            i += 1
    return items

def oxygen(lst):    
    return int(calc(lst, lst, most_common), 2)    

def c02(lst):
    return int(calc(lst, lst, least_common), 2)

def calc(lst, orglst, func):
    if(len(lst) == 1):
        return orglst[0]

    newlst = []
    neworglst = []
    bits = create_list(lst)[0]
    c = func(bits)
    i = 0
    for bit in bits:
        if(bit == c):
            neworglst.append(orglst[i])
            newlst.append(lst[i][1:])
        i += 1

    return calc(newlst, neworglst, func)


lines = []
gamma = ""
epsilon = ""
with open('p1.in') as file:
    for line in file:
        l = line.strip()
        lines.append(l)

print(oxygen(lines) * c02(lines))