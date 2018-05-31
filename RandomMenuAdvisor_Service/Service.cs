﻿using System;
using System.ServiceModel;
using ServiceLibrary;

namespace RandomMenuAdvisor_Service
{
    /// <summary>
    /// 서비스를 핸들링하는 클래스입니다.
    /// </summary>
    public class Service
    {
        private ServiceHost host;
        private bool isRunning;

        public bool IsRunning { get => isRunning; }

        public Service()
        {

        }

        public bool StartService()
        {
            host = new ServiceHost(typeof(MenuAdvisorService),
                                    new Uri("http://192.168.1.69:5000/wcf/example/randommenuadvisor_service"));

            host.Open();
            isRunning = true;
            return true;

        }

        public bool StopService()
        {
            host.Close();
            isRunning = false;
            return true;
        }
    }
}