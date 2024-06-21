#  78行填写订阅链接
redir-port: 7892      # Redir 端口
mixed-port: 7893      # HTTP和SOCKS5端口
geodata-mode: false   # 使用geoip.dat数据库 (默认：false使用mmdb数据库)
tcp-concurrent: true  # TCP 并发连接所有 IP, 将使用最快握手的 TCP
allow-lan: false      # 代理共享
bind-address: "*"     # 仅在将allow-lan设置为true时适用
                      # #"*": 绑定所有IP地址
find-process-mode: strict           #匹配所有进程（always/strict/off）
ipv6: false           # IPv6 开关，关闭阻断所有 IPv6 链接和屏蔽 DNS 请求 AAAA 记录
mode: rule            # 规则模式：rule / global/ direct/ script
log-level: info       # 日志输出级别 (5个级别：silent / error / warning / info / debug）
external-controller: 0.0.0.0:9093   #外部控制器,可以使用 RESTful API 来控制你的 clash 内核
global-client-fingerprint: chrome   #全局 TLS 指纹，优先低于 proxy 内的 client-fingerprint
                                    #可选： "chrome","firefox","safari","ios","random","none" options.

geox-url:
  geoip: "https://hub.gitmirror.com/https://github.com/MetaCubeX/meta-rules-dat/releases/download/latest/geoip.dat"
  mmdb: "https://raw.githubusercontent.com/NobyDa/geoip/release/Private-GeoIP-CN.mmdb"

profile:
  store-selected: true # 存储 select 选择记录
  store-fake-ip: true  # 持久化 fake-ip
  
sniffer:               # 嗅探域名 可选配置
  enable: true
  parse-pure-ip: true  # 是否使用嗅探结果作为实际访问，默认 true
  sniff:
    TLS:               # TLS 默认嗅探 443
      ports: [443, 8443]
    HTTP:
      ports: [80, 8080-8880]
      override-destination: true
      
tun:                   
  enable: true
  stack: system        # system/gvisor/lwip
                       # tun模式堆栈,如无使用问题,建议使用 system 栈;
                       # MacOS 用户推荐 gvisor栈,IOS无法使用system栈
  dns-hijack:          # dns劫持,一般设置为 any:53 即可, 即劫持所有53端口的udp流量
     - 'any:53'
     - "tcp://any:53"
  strict_route: true   # 将所有连接路由到tun来防止泄漏，但你的设备将无法其他设备被访问
  auto-route: true     # 自动设置全局路由，可以自动将全局流量路由进入tun网卡。
  auto-detect-interface: true     # 自动识别出口网卡

dns:
  enable: false             # 禁用系统 DNS
  ipv6: false              # 关闭 IPv6
  enhanced-mode: fake-ip   # 增强模式：redir-host或fake-ip
  listen: 0.0.0.0:53       # DNS监听地址
  fake-ip-range: 198.18.0.1/16    # Fake-IP解析地址池
  # fake ip 白名单列表'以下地址不会下发fakeip映射用于连接 
  fake-ip-filter:
    - '*'
    - '+.lan'
    - '+.local'
  default-nameserver:
    - 223.5.5.5
    - 119.29.29.29
    - 114.114.114.114
    - '[2402:4e00::]'
    - '[2400:3200::1]'
  nameserver:
    - 'tls://8.8.4.4#dns'
    - 'tls://1.0.0.1#dns'
    - 'tls://[2001:4860:4860::8844]#dns'
    - 'tls://[2606:4700:4700::1001]#dns'
  proxy-server-nameserver:
    - https://doh.pub/dns-query
  nameserver-policy:
    "geosite:cn,private":
      - https://doh.pub/dns-query
      - https://dns.alidns.com/dns-query

#  机场订阅
proxy-providers:
 conf1:
    type: http
    # 机场订阅链接
    url: https://1.xn--xc3ao8r.top/api/v1/client/subscribe?token=750eed56ea3e226e5098dd67f8fbc8d3
    path: ./proxies/airport1.yaml
    interval: 86400

# 策略组
proxy-groups:
  - {name: 🔚Final, type: select, proxies: [☪️延迟优选, ✅手动选择, 🇰🇷 韩国节点, 🇭🇰 香港节点, 🇹🇼 台湾节点, 🇯🇵 日本节点, 🇸🇬 狮城节点, 🇺🇸 美国节点]}
  - {name: ✅手动选择, type: select, use: [conf1]}
  - {name: ☪️延迟优选, type: url-test, lazy: true,  url: http://www.google.com/generate_204, interval: 900, use: [conf1]}
  - {name: 🍋BiliBili, type: select, proxies: [DIRECT, 🇭🇰 香港节点, 🇯🇵 日本节点]}
  - {name: 🍎YouTube, type: select, proxies: [✅手动选择, 🇭🇰 香港节点, 🇯🇵 日本节点, 🇹🇼 台湾节点, 🇸🇬 狮城节点, 🇺🇸 美国节点, 🇰🇷 韩国节点]}
  - {name: 🍇Microsoft, type: select, proxies: [DIRECT, ✅手动选择, 🇭🇰 香港节点, 🇯🇵 日本节点]}
  - {name: 🍰Google, type: select, proxies: [✅手动选择, 🇭🇰 香港节点, 🇹🇼 台湾节点, 🇯🇵 日本节点, 🇸🇬 狮城节点, 🇺🇸 美国节点]}
  - {name: 🍓TikTok, type: select, proxies: [✅手动选择, 🇹🇼 台湾节点, 🇯🇵 日本节点, 🇸🇬 狮城节点, 🇺🇸 美国节点, 🇰🇷 韩国节点]}
  - {name: 🍉Spotify, type: select, proxies: [✅手动选择, 🇭🇰 香港节点, 🇹🇼 台湾节点, 🇯🇵 日本节点, 🇸🇬 狮城节点, 🇺🇸 美国节点, 🇰🇷 韩国节点]}
  - {name: 🍈GlobalMedia, type: select, proxies: [☪️延迟优选, ✅手动选择, 🇭🇰 香港节点, 🇹🇼 台湾节点, 🇯🇵 日本节点, 🇸🇬 狮城节点, 🇺🇸 美国节点, 🇰🇷 韩国节点]}
 
 # ----------------国家或地区策略组---------------------
  # 若有多个机场，可以使用 `include-all-providers: true` 代替 `use: [conf1]`
  - {name: 🇭🇰 香港节点, type: select, use: [conf1], filter: "香港|港|HK|(?i)HongKong"}
  - {name: 🇹🇼 台湾节点, type: select, use: [conf1], filter: "台湾|湾|TW|(?i)Taiwan"}
  - {name: 🇯🇵 日本节点, type: select, use: [conf1], filter: "日本|日|JP|(?i)Japan"}
  - {name: 🇸🇬 狮城节点, type: select, use: [conf1], filter: "新加坡|狮|SG|(?i)Singapore"}
  - {name: 🇺🇸 美国节点, type: select, use: [conf1], filter: "美国|美|US|(?i)UnitedStates"}
  - {name: 🇰🇷 韩国节点, type: select, use: [conf1], filter: "韩国|韩|KR|(?i)Korea"}

  
# 规则集（yaml 文件每天自动更新）
rule-providers:

  BiliBili:
    type: http
    behavior: classical
    format: yaml
    path: ./BiliBili/BiliBili_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/BiliBili/BiliBili_No_Resolve.yaml"
    interval: 86400

  Spotify:
    type: http
    behavior: classical
    format: yaml
    path: ./Spotify/Spotify_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/Spotify/Spotify_No_Resolve.yaml"
    interval: 86400

  TikTok:
    type: http
    behavior: classical
    format: yaml
    path: ./TikTok/TikTok_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/TikTok/TikTok_No_Resolve.yaml"
    interval: 86400 

  YouTube:
    type: http
    behavior: classical
    format: yaml
    path: ./YouTube/YouTube_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/YouTube/YouTube_No_Resolve.yaml"
    interval: 86400

  Telegram:
    type: http
    behavior: classical
    format: yaml
    path: ./Telegram/Telegram_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/Telegram/Telegram_No_Resolve.yaml"
    interval: 86400
 
  Microsoft:
    type: http
    behavior: classical
    format: yaml
    path: ./Microsoft/Microsoft_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/Microsoft/Microsoft_No_Resolve.yaml"
    interval: 86400

  Google:
    type: http
    behavior: classical
    format: yaml
    path: ./Google/Google_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/Google/Google_No_Resolve.yaml"
    interval: 86400

  GlobalMedia:
    type: http
    behavior: classical
    format: yaml
    path: ./GlobalMedia/GlobalMedia_Classical_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/GlobalMedia/GlobalMedia_Classical_No_Resolve.yaml"
    interval: 86400

  China:
    type: http
    behavior: classical
    format: yaml
    path: ./ChinaMax/ChinaMax_Classical_No_IPv6_No_Resolve.yaml
    url: "https://raw.githubusercontent.com/blackmatrix7/ios_rule_script/master/rule/Clash/ChinaMax/ChinaMax_Classical_No_IPv6_No_Resolve.yaml"
    interval: 86400

# 规则
rules:
  - RULE-SET,BiliBili,🍋BiliBili 
  - RULE-SET,TikTok,🍓TikTok  
  - RULE-SET,Spotify,🍉Spotify  
  - RULE-SET,YouTube,🍎YouTube   
  - RULE-SET,Microsoft,🍇Microsoft
  - RULE-SET,Google,🍰Google
  - RULE-SET,GlobalMedia,🍈GlobalMedia  
  - RULE-SET,China,DIRECT
  - GEOIP,CN,DIRECT,no-resolve
  - MATCH,🔚Final

script:
  shortcuts:
    quic: network == 'udp' and dst_port == 443

