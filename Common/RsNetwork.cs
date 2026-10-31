using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Common {
    public static class RsNetwork {
        /// <summary>
        /// ローカルマシンのIPv4アドレスを取得する
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPv4() {
            foreach(var ni in NetworkInterface.GetAllNetworkInterfaces()) {
                // 実際に通信可能な NIC のみ採用
                if(ni.OperationalStatus != OperationalStatus.Up)
                    continue;
                var ipProps = ni.GetIPProperties();
                foreach(var ua in ipProps.UnicastAddresses) {
                    if(ua.Address.AddressFamily == AddressFamily.InterNetwork) {
                        // 169.254.x.x（APIPA）は除外
                        if(!ua.Address.ToString().StartsWith("169.254"))
                            return ua.Address.ToString();
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// ローカルマシンのデフォルトゲートウェイを取得する
        /// </summary>
        /// <returns></returns>
        public static string? GetDefaultGateway() {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic =>
                    nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback
                )
                .SelectMany(nic => nic.GetIPProperties().GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a != null && a.AddressFamily == AddressFamily.InterNetwork)
                .Select(a => a.ToString())
                .FirstOrDefault();
        }
    }
}
