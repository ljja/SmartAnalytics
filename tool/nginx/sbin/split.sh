
#30 * * * * ntpdate time.nist.gov
#59 * * * * sleep 50; /usr/local/openresty/nginx/sbin/split.sh

_prefix="/usr/local/openresty/nginx"

time=`date +%Y%m%d%H`

mv ${_prefix}/logs/ma.log ${_prefix}/logs/ma/ma-${time}.log

kill -USR1 `cat ${_prefix}/logs/nginx.pid`
