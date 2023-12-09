def FindNextNumber(sequence):
    done = True
    for num in sequence:
        if num != 0:
            done = False
            break

    if done:
        return 0

    diffs = []
    for i in range(0, len(sequence) - 1):
        diff = sequence[i + 1] - sequence[i]
        diffs.append(diff)

    return FindNextNumber(diffs) + sequence.pop()

filename = "input.in"
total = 0
with open(filename) as file:
    for line in file:
        seq = line.strip().split()
        seq = list(map(int, seq))
        seq.reverse()
        total += FindNextNumber(seq)
print(total)
