import _mssql
import dataset

print('passed')
conn = _mssql.connect(server='172.16.0.106', user='saPrime', password='saPrime', \
    database='SafewayPrime')
conn.execute_query('SELECT * FROM instance')
for row in conn:
    print ("Name=" +  row['InstanceName'])