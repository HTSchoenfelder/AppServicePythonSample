import sys
import os

path = sys.argv[1]
print(path)

fileName = sys.argv[2]
print(fileName)

fullPath = os.path.join(path, fileName)
print(fullPath)

content = sys.argv[3]
print(content)

with open(fullPath, 'w') as f:
    f.write(content)