
#0 06 1 * * /usr/local/openresty/nginx/sbin/tarlogs.sh

_prefix="/usr/local/openresty/nginx/logs"

time=`date -d "-1 month" +%Y%m`

tar -czvf ${_prefix}/logbak/ma-${time}.tar.gz ${_prefix}/ma/ma-${time}*.log

rm -rf ${_prefix}/ma/ma-${time}*.log

